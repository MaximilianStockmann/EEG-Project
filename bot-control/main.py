#!/usr/bin/env python
# -*- coding: utf-8 -*-

import server as S
import signal as SG
import sys
import RPi.GPIO as GPIO

PinRB = 19 #0 RB
PinRF = 20 #1 RF
PinLB = 21 #2 LB
PinLF = 26 #3 LF
RF, RB, LF, LB = 0, 1, 2, 3

Pins = (PinRF, PinRB, PinLF, PinLB)
PWMs = []

SPEED = 20.0
ROTATE_FACTOR = 0.5
STRIAGHT_FACTOR = 0.95

def init():

    S.setClientLostCallback(clienLostCB)
    S.setSpeedCallback(changeSpeed)

    for sig in (SG.SIGABRT, SG.SIGILL, SG.SIGINT, SG.SIGSEGV, SG.SIGTERM):
        SG.signal(sig, exit)

    print "Init Pins:"
    GPIO.setmode(GPIO.BCM)
    for k, p in enumerate(Pins):
        print k,p
        GPIO.setup(p, GPIO.OUT)
        PWMs.insert(k, GPIO.PWM(p, 1000))
        PWMs[k].start(0.0)

    print "Open Server:"
    S.openServer()


def exit(a = 0,b = 0):
    print "Exit Programm",a,b
    S.closeServer()

    for p in PWMs:
        p.stop()
    GPIO.cleanup()
    sys.exit(0)


def changeSpeed(s):
    global SPEED
    print "New SPEED: ",s
    SPEED = s


def clienLostCB():
    driveStop()
    print "Client lost, search for new one:"
    S.searchForClient()



def driveStop():
    for p in PWMs:
        p.ChangeDutyCycle(0.0)

def driveForward():
    driveStop()
    PWMs[RF].ChangeDutyCycle(SPEED)
    PWMs[LF].ChangeDutyCycle(SPEED*STRIAGHT_FACTOR)

def driveLeft():
    driveStop()
    PWMs[RF].ChangeDutyCycle(SPEED*ROTATE_FACTOR)
    PWMs[LB].ChangeDutyCycle(SPEED*ROTATE_FACTOR)

def driveRight():
    driveStop()
    PWMs[LF].ChangeDutyCycle(SPEED*ROTATE_FACTOR)
    PWMs[RB].ChangeDutyCycle(SPEED*ROTATE_FACTOR)

def driveBackward():
    driveStop()
    PWMs[RB].ChangeDutyCycle(SPEED)
    PWMs[LB].ChangeDutyCycle(SPEED*STRIAGHT_FACTOR)


if __name__ == '__main__':

    init()
    while 1:
        c = S.waitForCommand()
        
        if c == "stop":
            driveStop()
            continue
        if c == "forward":
            driveForward()
            continue
        if c == "backward":
            driveBackward()
            continue
        if c == "left":
            driveLeft()
            continue
        if c == "right":
            driveRight()
            continue
        if c == "BREAK":
            break
