<?php
$sub_menu = '300910';
include_once('./_common.php');
include_once(G5_EDITOR_LIB);

$html_title = "검사지";

if ($w == "u")
{
    $html_title .= " 수정";
    $readonly = " readonly";

    $sql = " select * from g5_qstnivstm where qstn_grop_cd = '$qstn_grop_cd' ";
    $row = sql_fetch($sql);

    if (!$row['qstn_grop_cd'])
        alert('등록된 자료가 없습니다.');

	$vald_strt_dd = substr($row['vald_strt_dd'], 0, 4) . "-" . substr($row['vald_strt_dd'], 4, 2) . "-" . substr($row['vald_strt_dd'], 6, 2);
	$vald_end_dd  = substr($row['vald_end_dd'], 0, 4) . "-" . substr($row['vald_end_dd'], 4, 2) . "-" . substr($row['vald_end_dd'], 6, 2);
}
else
{
    $html_title .= ' 입력';
	$vald_strt_dd = G5_TIME_YMD;
	$vald_end_dd = '2999-12-31';
}

$g5['title'] = $html_title.' 관리';

include_once(G5_ADMIN_PATH.'/admin.head.php');
include_once(G5_PLUGIN_PATH.'/jquery-ui/datepicker.php');

auth_check($auth[$sub_menu], "w");

$token = get_admin_token();

/*
// 상단, 하단 파일경로 필드 추가
if(!sql_query(" select co_include_head from {$g5['content_table']} limit 1 ", false)) {
    $sql = " ALTER TABLE `{$g5['content_table']}`  ADD `co_include_head` VARCHAR( 255 ) NOT NULL ,
                                                    ADD `co_include_tail` VARCHAR( 255 ) NOT NULL ";
    sql_query($sql, false);
}

// html purifier 사용여부 필드
if(!sql_query(" select co_tag_filter_use from {$g5['content_table']} limit 1 ", false)) {
    sql_query(" ALTER TABLE `{$g5['content_table']}`
                    ADD `co_tag_filter_use` tinyint(4) NOT NULL DEFAULT '0' AFTER `co_content` ", true);
    sql_query(" update {$g5['content_table']} set co_tag_filter_use = '1' ");
}

// 모바일 내용 추가
if(!sql_query(" select co_mobile_content from {$g5['content_table']} limit 1", false)) {
    sql_query(" ALTER TABLE `{$g5['content_table']}`
                    ADD `co_mobile_content` longtext NOT NULL AFTER `co_content` ", true);
}

// 스킨 설정 추가
if(!sql_query(" select co_skin from {$g5['content_table']} limit 1 ", false)) {
    sql_query(" ALTER TABLE `{$g5['content_table']}`
                    ADD `co_skin` varchar(255) NOT NULL DEFAULT '' AFTER `co_mobile_content`,
                    ADD `co_mobile_skin` varchar(255) NOT NULL DEFAULT '' AFTER `co_skin` ", true);
    sql_query(" update {$g5['content_table']} set co_skin = 'basic', co_mobile_skin = 'basic' ");
}
*/

?>

<form name="frmcontentform" action="./questionformupdate.php" onsubmit="return frmcontentform_check(this);" method="post" enctype="MULTIPART/FORM-DATA" >
<input type="hidden" name="w" value="<?php echo $w; ?>">
<input type="hidden" name="co_html" value="1">
<input type="hidden" name="token" value="<?=$token?>">
<input type="hidden" name="qstn_grop_cd" value="<?=$row['qstn_grop_cd']?>">

<div class="tbl_frm01 tbl_wrap">
    <table>
    <caption><?php echo $g5['title']; ?> 목록</caption>
    <colgroup>
        <col class="grid_4">
        <col>
    </colgroup>
    <tbody>
    <tr>
        <th scope="row"><label for="co_id">질문코드</label></th>
        <td>
            <?=($row['qstn_grop_cd'] == "") ? help('자동생성 됩니다.') :  $row['qstn_grop_cd']; ?>
        </td>
    </tr>
    <tr>
        <th scope="row"><label for="qstn_grop_titl">설문지제목</label></th>
        <td><input type="text" name="qstn_grop_titl" value="<?php echo htmlspecialchars2($row['qstn_grop_titl']); ?>" id="qstn_grop_titl" required class="frm_input required" size="90"></td>
    </tr>
	<tr>
        <th scope="row"><label for="vald_strt_dd">유효일자</label></th>
        <td>
		<input type="text" name="vald_strt_dd" value="<?php echo $vald_strt_dd ?>" id="vald_strt_dd" class="frm_input" size="11" maxlength="10">
		<label for="vald_strt_dd" class="sound_only">시작일</label>
		</td>
    </tr>
	<tr>
        <th scope="row"><label for="vald_end_dd">종료일자</label></th>
        <td>
		<input type="text" name="vald_end_dd" value="<?php echo $vald_end_dd ?>" id="vald_end_dd" class="frm_input" size="11" maxlength="10">
	    <label for="vald_end_dd" class="sound_only">종료일</label>	
		</td>
    </tr>
	<tr>
        <th scope="row"><label for="head">머리말</label></th>
		<td><?php echo editor_html('head', get_text($row['head'], 0)); ?></td>
    </tr>
	<tr>
        <th scope="row"><label for="rslt">결과</label></th>
		<td><?php echo editor_html('rslt', get_text($row['rslt'], 0)); ?></td>
    </tr>
    </tbody>
    </table>
</div>

<div class="btn_fixed_top">
    <a href="./questionlist.php" class="btn btn_02">목록</a>
    <input type="submit" value="확인" class="btn btn_submit" accesskey="s">
</div>

</form>

<script>

$(function(){
    $("#vald_strt_dd, #vald_end_dd").datepicker({ changeMonth: true, changeYear: true, dateFormat: "yy-mm-dd", showButtonPanel: true, yearRange: "c-99:c+99", maxDate: "" });
});

function frmcontentform_check(f)
{
    errmsg = "";
    errfld = "";

	
	<?php echo get_editor_js('head'); ?>

	<?php echo get_editor_js('rslt'); ?>

    check_field(f.rqst_titl, "설문지 제목을 입력하세요.");

    if (errmsg != "") {
        alert(errmsg);
        errfld.focus();
        return false;
    }
    return true;
}

</script>

<?php
include_once (G5_ADMIN_PATH.'/admin.tail.php');
?>
