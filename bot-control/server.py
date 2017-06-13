#!/usr/bin/env python
# -*- coding: utf-8 -*-
'''
server.py

Creates an TCP connection and recives data.

Author: Marcel Gehre
'''

import socket

# constans
TCP_IP = ''
TCP_PORT = 13337
BUFFER_SIZE = 20  # Normally 1024, but we want fast response
# command enums
C_CLIENT_LOST = "LOST"
C_NO_COMMAND = "NO_CMD"

# callbacks
def default():
    pass
speedCallback = default
clientLostCallback = default

# connection element
conn = False

def openServer():
    global s
    global conn
    try:
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        s.bind((TCP_IP, TCP_PORT))
        s.listen(1)
    except Exception as e:
        print e
    searchForClient()

def searchForClient():
    global conn
    global s
    conn, addr = s.accept()
    print 'Connection address:', addr

def closeServer():
    global conn
    if not conn: return;
    try:
        conn.sendall("Connection lost: Error occurred!")
    except:
        pass
    conn.close()

# response to client
def okStatus():
    global conn
    conn.sendall("ok")

def waitForCommand():
    global conn
    global clientLostCallback
    global speedCallback

    # wait for data form client
    try:
        data = conn.recv(BUFFER_SIZE)
    except SystemExit:
        return "BREAK"
    except:
        clientLostCallback()
        return C_CLIENT_LOST

    if not data:
        clientLostCallback()
        return C_CLIENT_LOST

    # check for speed command
    if "speed" in data and len(data) > 6:
        try:
            speedCallback(float(data[6:]))
            okStatus()
            return C_NO_COMMAND
        except ValueError:
            okStatus()
            return C_NO_COMMAND
    # return data
    okStatus()
    return data

# execute if speed command is detected
def setSpeedCallback(f):
    global speedCallback
    speedCallback = f

# execute if client is lost
def setClientLostCallback(f):
    global clientLostCallback
    clientLostCallback = f


# only for testing
if __name__ == '__main__':
    print "Server main"
    openServer()
    try:
        while 1:
            c = waitForCommand()
            if c == C_CLIENT_LOST:
                print "C_CLIENT_LOST"
                searchForClient()
            print c
    except KeyboardInterrupt:
        closeServer()
    # except socket.error as msg:
    #     closeServer()
    except:
        closeServer()
