#!/usr/bin/env python
# -*- coding: utf-8 -*-

# import required modules
import RPi.GPIO as GPIO
import time

# define GPIO pin for PWM
PINa = 20
PINb = 21
PINc = 19
PINd = 26


# main function


def main():
    try:
        # use GPIO pin numbering convention
        GPIO.setmode(GPIO.BCM)

        # set up GPIO pin for output
        GPIO.setup(PINa, GPIO.OUT)
        GPIO.setup(PINb, GPIO.OUT)
        GPIO.setup(PINc, GPIO.OUT)
        GPIO.setup(PINd, GPIO.OUT)

        # create object for PWM at 1 Hz
        a = GPIO.PWM(PINa, 1000)
        b = GPIO.PWM(PINb, 1000)
        c = GPIO.PWM(PINc, 1000)
        d = GPIO.PWM(PINd, 1000)

        # start PWM with 50% duty cycle
        a.start(0.0)
        b.start(0.0)
        c.start(0.0)
        d.start(0.0)
        while(1):
            txt = raw_input()
            if "a" in txt:
                a.ChangeDutyCycle(float(txt[1:]))
            if "b" in txt:
                b.ChangeDutyCycle(float(txt[1:]))
            if "c" in txt:
                c.ChangeDutyCycle(float(txt[1:]))
            if "d" in txt:
                d.ChangeDutyCycle(float(txt[1:]))

    # reset GPIO settings if user pressed Ctrl+C
    except KeyboardInterrupt:
        print("Execution stopped by user")
        a.stop()
        b.stop()
        c.stop()
        d.stop()
        GPIO.cleanup()


if __name__ == '__main__':
    main()
