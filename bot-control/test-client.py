#!/usr/bin/env python
# -*- coding: utf-8 -*-

# Echo client program
import socket
import sys

manuel = False

HOST = '127.0.0.1'    # The remote host
if len(sys.argv) > 1:
    print "Custom connection: " + sys.argv[1]
    HOST = sys.argv[1]
if len(sys.argv) > 2 and sys.argv[2] == "manuel":
    manuel = True


PORT = 13337              # The same port as used by the server
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect((HOST, PORT))


def send(arg):
    global s
    print arg
    s.sendall(arg)


if manuel:
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
        sys.exit(0)




try:
    while 1:
        s.sendall(raw_input())
        print "Status: " + s.recv(20)
except KeyboardInterrupt:
    s.close()
except:
    print "No Server"
    s.close()
