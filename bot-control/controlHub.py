#!/usr/bin/env python
# -*- coding: utf-8 -*-
import signal as SG
import sys
import RPi.GPIO as GPIO
import subprocess
import time
import os
import rgb_led as RGB

TimeKeeper = 10.0
Server_pid = 0
ButtonPressed = False
Color = 1
actColor = 0xff0000

BtnPin = 18
Gpin = 12
Rpin = 13


def setup():
    for sig in (SG.SIGABRT, SG.SIGILL, SG.SIGINT, SG.SIGSEGV, SG.SIGTERM):
        SG.signal(sig, exit)
        GPIO.setmode(GPIO.BCM)
        # GPIO.setup(Gpin, GPIO.OUT)     # Set Green Led Pin mode to output
        # GPIO.setup(Rpin, GPIO.OUT)     # Set Red Led Pin mode to output

        # Set BtnPin's mode is input, and pull up to high level(3.3V)
        GPIO.setup(BtnPin, GPIO.IN, pull_up_down=GPIO.PUD_UP)
        GPIO.remove_event_detect(BtnPin)
        GPIO.add_event_detect(BtnPin, GPIO.BOTH, callback=detect)

        RGB.setup(23,24,25)
        RGB.setColor(actColor)


def detect(chn):
    global TimeKeeper
    global ButtonPressed
    global Server_pid
    global Color
    global actColor

    print GPIO.input(BtnPin)
    if GPIO.input(BtnPin) == 0:
        TimeKeeper = 0.0
        ButtonPressed = True

    if GPIO.input(BtnPin) == 1:
        ButtonPressed = False
        if TimeKeeper >= 3:
            p = subprocess.Popen("sudo bash /home/pi/Documents/EEG-Project/bot-control/startWlan.sh",shell=True)
            print "WLAN started, pid: " + str(p.pid)
            oldColor = actColor
            actColor = 0xff00ff
            time.sleep(1)
            actColor = oldColor
            Color = 5
        elif TimeKeeper >= 1:
            if Server_pid == 0:

                p = subprocess.Popen("python /home/pi/Documents/EEG-Project/bot-control/main.py > /home/pi/Documents/EEG-Project/bot-control/server.log",shell=True,preexec_fn=os.setsid)
                Server_pid = p.pid
                print "Server started, pid: " + str(Server_pid)
                actColor = 0x00ff00

            else:
                print "Closing server, pid: " + str(Server_pid)
                try:
                    os.killpg(Server_pid, SG.SIGTERM)
                except Exception as e:
                    print e
                Server_pid = 0
                actColor = 0xff0000



            #subprocess.Popen("python main.py > some.txt", shell=True)



def exit(a=0, b=0):
    global Server_pid
    print "\b\bExit Programm", a, b
    if Server_pid > 0: os.killpg(Server_pid, SG.SIGTERM)

    GPIO.remove_event_detect(BtnPin)
    RGB.destroy()
    #GPIO.cleanup()
    sys.exit(0)


if __name__ == '__main__':
    setup()
    #print subprocess.Popen("echo Hello World", shell=True, stdout=subprocess.PIPE).stdout.read()
    while 1:
        time.sleep(0.1)
        if ButtonPressed:
            TimeKeeper = TimeKeeper + 0.1
            if TimeKeeper >= 3 and Color == 2:
                RGB.setColor(0x0000ff)
                Color = 3
                #print "orange"
                pass
            elif TimeKeeper >= 1 and Color == 1:
                RGB.setColor(0xffa500)
                Color = 2;
                #print "yellow"
                pass
        elif Color != 1:
            Color = 1
            RGB.setColor(actColor)
