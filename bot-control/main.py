#!/usr/bin/env python
# -*- coding: utf-8 -*-

import server as S




try:
    S.openServer()
    S.setClientLostCallback(S.searchForClient)
    while 1:
        c = S.waitForCommand()
        print c
except KeyboardInterrupt:
    S.closeServer()
# except:
#     S.closeServer()


# TODO
# erzwungenes abbrechen erfassen
# button um programm zu starten/neustarten?
