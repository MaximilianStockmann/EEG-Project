#!/usr/bin/env python
# -*- coding: utf-8 -*-

# Echo client program
import socket



HOST = '192.168.178.33'    # The remote host
PORT = 13337              # The same port as used by the server
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect((HOST, PORT))


def send(arg):
    print arg
    s.sendall(arg)

import pygame
pygame.init()
try:
    while True:

        for event in pygame.event.get():
            if event.type == pygame.KEYDOWN:
                if event.key == pygame.K_UP:
                    send("forward")
                elif event.key == pygame.K_DOWN:
                    send("backward")
                elif event.key == pygame.K_LEFT:
                    send("left")
                elif event.key == pygame.K_RIGHT:
                    send("right")

            elif event.type == pygame.KEYUP:
                send("stop")
except KeyboardInterrupt:
    s.close()

try:
    while 1:
        s.sendall(raw_input())
        pass
except KeyboardInterrupt:
    s.close()
