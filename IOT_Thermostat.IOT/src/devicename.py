def getEthName():
    # Get name of the Ethernet interface
    try:
        for root, dirs, files in os.walk('/sys/class/net'):
            for dir in dirs:
                if dir[:3] == 'enx' or dir[:3] == 'eth':
                    interface = dir
    except:
        interface = "None"
    return interface


def getMAC(interface='eth0'):
    # Return the MAC address of the specified interface
    try:
        str = open('/sys/class/net/%s/address' % interface).read()
    except:
        str = "00:00:00:00:00:00"
    return str[0:17]


def getDeviceName():
    ethName = getEthName()
    ethMAC = getMAC(ethName)
    return ethMAC
