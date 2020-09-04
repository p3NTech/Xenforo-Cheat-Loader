<?php

function file_force_download($file) {
  if (file_exists($file)) {
    // flush the PHP output buffer to avoid overflowing the memory allocated for the script
    // if this is not done, the file will be read into memory completely!
    if (ob_get_level()) {
      ob_end_clean();
    }
    // make the browser show the file save window
    echo header('Content-Description: File Transfer');
    echo header('Content-Type: application/octet-stream');
    echo header('Content-Disposition: attachment; filename=' . basename($file));
    echo header('Content-Transfer-Encoding: binary');
    echo header('Expires: 0');
    echo header('Cache-Control: must-revalidate');
    echo header('Pragma: public');
    echo header('Content-Length: ' . filesize($file));
    // read the file and send it to the user
    if ($fd = fopen($file, 'rb')) {
      while (!feof($fd)) {
        print fread($fd, 1024);
      }
      fclose($fd);
    }
    exit;
  }
}

if ( isset( $_GET[ "info" ] ) )
{
	echo "useless updates since 2020";
}
if ( isset( $_GET[ "version" ] ) )
{
	echo "1.65";
}
if ( isset( $_GET[ "username" ] ) )
{
	require( 'src/XF.php' );
	XF::start( $fileDir );
	$app = \XF::setupApp( 'XF\App' );

	$ip = $app->request->getIp( );

	if ( isset( $_GET[ "password" ] ) )
	{
		$loginService = $app->service( 'XF:User\Login', $_GET[ "username" ], $ip );
		$userValidate = $loginService->validate( $_GET[ "password" ], $error );
		if( $userValidate )
		{
			echo "success";
		}
	}
	else
	{
		$finder = \XF::finder( 'XF:User' );
		$user = $finder->where( 'username', $_GET[ "username" ] )->fetchOne( );

		if ( isset( $_GET[ "uid" ] ) )
		{
			echo $user[ "user_id" ];
		}
		else if ( isset( $_GET[ "avatar" ] ) )
		{
			echo "https://cheat.domain/forums/data/avatars/l/0/" . $user[ "user_id" ] . ".jpg";
		}
		else if ( isset( $_GET[ "profile" ] ) )
		{
			echo "https://cheat.domain/forums/members/" . $user[ "username" ] . "." . $user[ "user_id" ];
		}
        else if ( isset( $_GET[ "loader" ] ) )
	    {
		if ( $user->isMemberOf( 5 || 3 || 4 ))
		{
		    file_force_download('loader.exe');
		   echo "sub :)";
		   
		}
	    else
	    {
		   echo "no sub:(";      
	    }
	    }	    
	    

		else if ( isset( $_GET[ "expire" ] ) )
		{
			$host = 'localhost'; //хост бд
		    $db   = 'drownpw_maindb'; //имя бд
		    $dbuser = 'drownpw_user47182'; //имя пользователя бд
		    $pass = 'T_HcIm84JtqW'; //пароль бд
		    $charset = 'utf8';

		    $dsn = "mysql:host=$host;dbname=$db;charset=$charset";
		    $opt = 
		    [
		        PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
		        PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
		        PDO::ATTR_EMULATE_PREPARES   => false,
		    ];

		    $pdo = new PDO( $dsn, $dbuser, $pass, $opt );

		    $stmt = $pdo->prepare( "SELECT end_date FROM xf_user_upgrade_active WHERE user_id=?" );
			$stmt->execute( array( $user[ "user_id" ] ) ); 
			$date = $stmt->fetch( );

			echo empty( $date[ "end_date" ] ) ? "nosub" :  date( " j, Y, g:i ", $date[ "end_date" ] ); //"d.m.Y"
		}
		

		else
		{
			if ( $user->isMemberOf( 5  ))
			{
				echo "sub"; 			    
			}
			else if ( $user->isMemberOf( 3  ))
			{
				echo "admin"; 			    
			}
			else if ( $user->isMemberOf( 2  ))
			{
				echo "nosub"; 			    
			}
			else if ( $user->isMemberOf( 4  ))
			{
				echo "moderator"; 			    
			}
		}
	}
}