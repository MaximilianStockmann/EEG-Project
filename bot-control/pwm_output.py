#!/usr/bin/env python
# -*- coding: utf-8 -*-

# import required modules
import RPi.GPIO as GPIO
import time

# define GPIO pin for PWM
PINa = 20
PINb = 21

# main function


def main():
    try:
        # use GPIO pin numbering convention
        GPIO.setmode(GPIO.BCM)

        # set up GPIO pin for output
        GPIO.setup(PINa, GPIO.OUT)
        GPIO.setup(PINb, GPIO.OUT)

        # create object for PWM at 1 Hz
        a = GPIO.PWM(PINa, 1000)
        b = GPIO.PWM(PINb, 1000)

        # start PWM with 50% duty cycle
        a.start(30.0)
        b.start(30.0)
        while(1):
            txt = raw_input()
            if "a" in txt:
                a.ChangeDutyCycle(float(txt[1:]))
            if "b" in txt:
                b.ChangeDutyCycle(float(txt[1:]))

    # reset GPIO settings if user pressed Ctrl+C
    except KeyboardInterrupt:
        print("Execution stopped by user")
        a.stop()
        b.stop()
        GPIO.cleanup()


if __name__ == '__main__':
    main()
