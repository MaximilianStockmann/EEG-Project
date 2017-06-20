#!/usr/bin/env python
# -*- coding: utf-8 -*-
'''
rgb_led.py

Controls an RGB-LED.

Author: Marcel Gehre
'''

import RPi.GPIO as GPIO

R = 23
G = 24
B = 25


def setup(Rpin, Gpin, Bpin, pwm=False):
    global pins
    global setColor
    global pwmMode
    pwmMode = pwm

    GPIO.setmode(GPIO.BCM)       # Numbers GPIOs by physical location

    pins = {'pin_R': Rpin, 'pin_G': Gpin, 'pin_B': Bpin}
    for i in pins:
        GPIO.setup(pins[i], GPIO.OUT)   # Set pins' mode is output
        GPIO.output(pins[i], GPIO.HIGH)  # Set pins to high(+3.3V) to off led

    if pwm:
        setupPWM(Rpin, Gpin, Bpin)
        setColor = setColorPWM
    else:
        setColor = setColorRGB


def setupPWM(Rpin, Gpin, Bpin):
    global p_R, p_G, p_B

    p_R = GPIO.PWM(pins['pin_R'], 5000)  # set Frequece to 5KHz
    p_G = GPIO.PWM(pins['pin_G'], 5000)
    p_B = GPIO.PWM(pins['pin_B'], 5000)

    p_R.start(100)      # Initial duty Cycle = 100(leds off)
    p_G.start(100)
    p_B.start(100)


def map(x, in_min, in_max, out_min, out_max):
    return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min


def off():
    for i in pins:
        GPIO.output(pins[i], GPIO.HIGH)    # Turn off all leds


def setColorPWM(col, debug=False):   # For example : col = 0x112233
    global p_R, p_G, p_B

    R_val = (col & 0xff0000) >> 16
    G_val = (col & 0x00ff00) >> 8
    B_val = (col & 0x0000ff) >> 0

    if debug:
        print("R_val {0:3d}, G_val {1:3d}, B_val {2:3d}".format(R_val, G_val, B_val))
    R_val = map(R_val, 0, 255, 0, 100)
    G_val = map(G_val, 0, 255, 0, 100)
    B_val = map(B_val, 0, 255, 0, 100)

    if debug:
        print("R_val {0:3d}, G_val {1:3d}, B_val {2:3d}".format(R_val, G_val, B_val))
    p_R.ChangeDutyCycle(100 - R_val)     # Change duty cycle
    p_G.ChangeDutyCycle(100 - G_val)
    p_B.ChangeDutyCycle(100 - B_val)


def setColorRGB(col, debug=False):
    global pins

    R_val = (col & 0xff0000) >> 16
    G_val = (col & 0x00ff00) >> 8
    B_val = (col & 0x0000ff) >> 0

    if debug:
        print("R_val {0:3d}, G_val {1:3d}, B_val {2:3d}".format(R_val, G_val, B_val))

    if R_val > 0:
        GPIO.output(pins['pin_R'], GPIO.LOW)
    else:
        GPIO.output(pins['pin_R'], GPIO.HIGH)
    if G_val > 0:
        GPIO.output(pins['pin_G'], GPIO.LOW)
    else:
        GPIO.output(pins['pin_G'], GPIO.HIGH)
    if B_val > 0:
        GPIO.output(pins['pin_B'], GPIO.LOW)
    else:
        GPIO.output(pins['pin_B'], GPIO.HIGH)


def loop():
    setColor(0xff00ff)
    while True:
        setColor(int(raw_input("Color:"), 16), True)
        # p_R.ChangeFrequency(int(raw_input("Color:")))


def destroy():
    if pwmMode:
        p_R.stop()
        p_G.stop()
        p_B.stop()
    off()
    GPIO.cleanup()


if __name__ == "__main__":
    try:
        setup(R, G, B,True)
        loop()
    except KeyboardInterrupt:
        destroy()
