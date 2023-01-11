<?php
define('G5_IS_ADMIN', true);
include_once ('../../common.php');

echo '<link rel="stylesheet" href="'.G5_ADMIN_URL.'/quest_invest/css/quest_invest.css">';

include_once(G5_ADMIN_PATH.'/admin.lib.php');

include_once(G5_ADMIN_PATH.'/admin.custom.lib.php'); //kjh �߰�

if( isset($token) ){
    $token = @htmlspecialchars(strip_tags($token), ENT_QUOTES);
}
?>
