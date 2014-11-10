makecert -a "sha256" -n "CN=shots.zxc.lv" -r -sv ZxcScreenShotCA.pvk ZxcScreenShotCA.cer
pvk2pfx.exe -pvk ZxcScreenShotCA.pvk -spc ZxcScreenShotCA.cer -pfx ZxcScreenShotCA.pfx
