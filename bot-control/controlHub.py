#!/usr/bin/env python
# -*- coding: utf-8 -*-
'''
controlHub.py

Controls wlan, server and shutdown manually
with a button.

Author: Marcel Gehre
'''
import signal as SG
import sys
import RPi.GPIO as GPIO
import subprocess
import time
import os
# pip imports
import psutil
# own file
import rgb_led as RGB

TimeKeeper = 10.0
Server_pid = 0
ButtonPressed = False
cntColor = 1
actColor = 0xff0000
debug = False

BtnPin = 18
Gpin = 12
Rpin = 13


def setup():
    global cntColor
    global debug
    # check for debug mode
    if len(sys.argv) > 1:
        debug = (sys.argv[1] == "debug")
        print sys.argv[1], debug
    # register exit functions
    for sig in (SG.SIGABRT, SG.SIGILL, SG.SIGINT, SG.SIGSEGV, SG.SIGTERM):
        SG.signal(sig, exit)
        pass

    # set GPIO pins
    GPIO.setmode(GPIO.BCM)
    # Set BtnPin's mode is input, and pull up to high level(3.3V)
    GPIO.setup(BtnPin, GPIO.IN, pull_up_down=GPIO.PUD_UP)
    GPIO.remove_event_detect(BtnPin)
    GPIO.add_event_detect(BtnPin, GPIO.BOTH, callback=detect)
    # set LED
    RGB.setup(23, 24, 25)
    RGB.setColor(actColor)
    # save last log file
    os.system("sudo mv /home/pi/Documents/EEG-Project/bot-control/server.log /home/pi/Documents/EEG-Project/bot-control/server2.log")
    # start server
    startServer()
    cntColor = 99



def detect(chn):
    global TimeKeeper
    global ButtonPressed

    if debug: print GPIO.input(BtnPin)

    if GPIO.input(BtnPin) == 0:
        # button pressed
        TimeKeeper = 0.0
        ButtonPressed = True

    elif GPIO.input(BtnPin) == 1:
        # button released
        ButtonPressed = False
        # ------ SHUTDOWN --------
        if TimeKeeper >= 5:
            if debug: print "Shutdown"
            os.system("sudo shutdown 0")
        # ------ WLAN --------
        elif TimeKeeper >= 3:
            startWLAN()
        # ------ SERVER --------
        elif TimeKeeper >= 1:
            # -- server ON --
            if Server_pid == 0:
                startServer()
            # -- server OFF --
            else:
                closeServer()


def startServer():
    global Server_pid
    global actColor

    p = subprocess.Popen("python /home/pi/Documents/EEG-Project/bot-control/main.py > /home/pi/Documents/EEG-Project/bot-control/server.log 2>&1", shell=True, preexec_fn=os.setsid)
    Server_pid = p.pid
    if debug: print "Server started, pid: " + str(Server_pid)
    actColor = 0x00ff00

def closeServer():
    global Server_pid
    global actColor

    if debug: print "Closing server, pid: " + str(Server_pid)
    try:
        os.killpg(Server_pid, SG.SIGTERM)
    except Exception as e:
        print e
    Server_pid = 0
    actColor = 0xff0000

def startWLAN():
    global actColor
    global cntColor

    p = subprocess.Popen("sudo bash /home/pi/Documents/EEG-Project/bot-control/startWlan.sh", shell=True)
    if debug: print "WLAN started, pid: " + str(p.pid)
    oldColor = actColor
    actColor = 0xff00ff
    time.sleep(1)
    actColor = oldColor
    cntColor = 99

def exit(a=0, b=0):
    if debug: print "\b\bExit Programm", a, b
    if Server_pid > 0:
        os.killpg(Server_pid, SG.SIGTERM)

    GPIO.remove_event_detect(BtnPin)
    RGB.destroy()
    # GPIO.cleanup()
    sys.exit(0)

def checkIfMainIsStillAlive():
    global Server_pid
    global actColor
    global cntColor

    # check if main.py is terminated, if yes make ist visible
    if psutil.Process(Server_pid).status() == "zombie":
        Server_pid = 0
        actColor = 0xff0000
        cntColor = 99
        if debug: print "main.py lost"


if __name__ == '__main__':
    setup()

    while 1:
        time.sleep(0.1)
        if ButtonPressed:
            TimeKeeper = TimeKeeper + 0.1
            if TimeKeeper >= 5 and cntColor == 3:
                # white color
                RGB.setColor(0xffffff)
            if TimeKeeper >= 3 and cntColor == 2:
                # blue color
                RGB.setColor(0x0000ff)
                cntColor = 3
            elif TimeKeeper >= 1 and cntColor == 1:
                # yellow color
                RGB.setColor(0xffff00)
                cntColor = 2
        elif cntColor != 1:
            cntColor = 1
            RGB.setColor(actColor)

        if Server_pid > 0:
            checkIfMainIsStillAlive()
