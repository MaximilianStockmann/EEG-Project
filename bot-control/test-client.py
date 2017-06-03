#!/usr/bin/env python
# -*- coding: utf-8 -*-

# Echo client program
import socket

HOST = '127.0.0.1'    # The remote host
PORT = 13337              # The same port as used by the server
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect((HOST, PORT))

try:
    while 1:
        s.sendall(raw_input())
except KeyboardInterrupt:
    s.close()
