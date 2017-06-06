#!/usr/bin/env python
# -*- coding: utf-8 -*-

# Echo client program
import socket
import sys

HOST = '127.0.0.1'    # The remote host
if len(sys.argv) > 1:
    print "Custom connection: " + sys.argv[1]
    HOST = sys.argv[1]
PORT = 13337              # The same port as used by the server


try:
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    s.connect((HOST, PORT))
    while 1:
        s.sendall(raw_input())
except KeyboardInterrupt:
    s.close()
except:
    s.close()
