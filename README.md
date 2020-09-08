# Xenforo-Cheat-Loader
cheat loader meant to be used with xenforo

# Tutorial

compile ????

upload web files???

sell paste p2c!!

# Actual Tutorial

--- Web files

Edit config.ini with your database name, password, and user. Leave database host and usertable alone.

Upload config.ini

In phpMyAdmin go to the xf_users table, and add a column called "hwid" that is a varchar with a limit of 255.

Upload hwid.php

Edit info.php with your website and forum link

Upload info.php

--- Loader files

If you are using .htaccess to require a user agent for your dll, include that in mainform.cs in the DownloadFileAsync headers.

Change the address string to your forum link

Secret can either be your user agent, or nothing, i don't care.



### MAKE SURE THAT THE VERSION IN INFO.PHP AND THE LOADER (FORM1.CS) MATCH. YOU WILL NOT BE ABLE TO USE IT IF THEY DO NOT.
