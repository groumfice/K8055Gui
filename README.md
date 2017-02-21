K8055Gui with RaspK8055 DLL
=====

Velleman k8055 or VM110 Windows driver and GUI sources built and tested for the new Raspberry Pi 3 (model B) under Windows 10 IOT.

![K8055Gui software interface](https://github.com/groumfice/K8055Gui/blob/master/K8055Gui.jpg)

This software allows access to Velleman's K8055 card. This software was developed to replace all other half-complete softwares for the k8055 board
under Windows 10 iot.

Velleman do not provide (not yet) a new recompiled DLL of k8055.dll for ARM platform.
After a lot of search, i didn't found any compatible dll to work on x64 Arm processor with the new Windows 10 IOT os.
I decided to code a brand new DLL as RaspK8055 (raspk8055.dll)

## What's new about RaspK8055 DLL ?

Prior, nothing. But, in fact, a lot of functions and the framework are different.

## How to build & install K8055Gui

2 ways :
- Simply add a reference to your Visual Studio Project and link to the DLL, build your project and run it.
- Upload the DLL to the Raspberry windows/syswow64 directory and use LoadLibrary to link to the DLL

## Thanks to :
Pjetur G. Hjaltason,
Martin Pischki,
Richard Hull
for your contributions

Bugs
----
Please report bug and error to groumfice@gmail.com

License
-------
[GPL](http://www.gnu.org/licenses/gpl.html)

References
----------
* http://www.velleman.eu/products/view/?country=be&lang=en&id=351346

* http://www.velleman.eu/downloads/0/user/usermanual_k8055_dll_uk.pdf

* http://george-smart.co.uk/wiki/Nokia_3310_LCD

* https://sites.google.com/site/vellemank8055/

* http://www.robert-arnold.de/cms/en/2010/10/zugriff-fur-nicht-root-user-auf-usb-board-k8055-unter-ubuntu-9-10-erlauben/
