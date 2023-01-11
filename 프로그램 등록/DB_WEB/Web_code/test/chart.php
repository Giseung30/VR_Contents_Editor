<?php
include_once('./_common.php');

define('_INDEX_', true);
if (!defined('_GNUBOARD_')) exit; // 개별 페이지 접근 불가

$g5['title'] = '차트';
include_once (G5_PATH.'/head.php');

?>

<!-- <canvas id="line-chart" width="800" height="450"></canvas>
<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>
<script type="text/javascript">

new Chart(document.getElementById("line-chart"), {
  type: 'line',
  data: {
    labels: [1500,1600,1700,1750,1800,1850,1900,1950,1999,2050],
    datasets: [{
        data: [86,114,106,106,107,111,133,221,783,2478],
        label: "Africa",
        borderColor: "#3e95cd",
        fill: false
      }, {
        data: [282,350,411,502,635,809,947,1402,3700,5267],
        label: "Asia",
        borderColor: "#8e5ea2",
        fill: false
      }, {
        data: [168,170,178,190,203,276,408,547,675,734],
        label: "Europe",
        borderColor: "#3cba9f",
        fill: false
      }, {
        data: [40,20,10,16,24,38,74,167,508,784],
        label: "Latin America",
        borderColor: "#e8c3b9",
        fill: false
      }, {
        data: [6,3,2,2,7,26,82,172,312,433],
        label: "North America",
        borderColor: "#c45850",
        fill: false
      }
    ]
  },
  options: {
    title: {
      display: true,
      text: 'World population per region (in millions)'
    }
  }
}); -->

<canvas id="line-chart" width="800" height="450"></canvas>
<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>
<script type="text/javascript">

<?php

$sql = " select count(*) as cnt from g5_qstnretn where qstn_grop_cd = $qstn_grop_cd AND content_no = $content_no";

$row_cnt = sql_fetch($sql);
$total_count = $row_cnt['cnt'];
$total_count = $total_count -1;

$sql = " select * from g5_qstnretn
where qstn_grop_cd = $qstn_grop_cd
AND content_no = $content_no order by rgst_dt asc
";

$result = sql_query($sql);

for($i=0;$row=sql_fetch_array($result);$i++){
  $result_data[$i]=$row['retn_value'];
  $play_time=$row['play_time'];
}

 ?>

new Chart(document.getElementById("line-chart"), {
  type: 'line',
  data: {
    labels: [<?php for($i=0;$i<$total_count;$i++){
      echo $i+1;
      echo ",";
    }echo $total_count+1; ?>],
    datasets: [{
        data: [<?php for($i=0;$i<$total_count;$i++){
          echo $result_data[$i];
          echo ",";
        }echo $result_data[$total_count];
         ?>],
        label: "<?php echo $member[mb_nick] ?>",
        borderColor: "#3e95cd",
        fill: false
      }
    ],
  },
  options: {
    title: {
      display: true,
      text: '컨텐츠 <?php echo $play_time ?> 번째 실행 결과'
    }
  }
});

</script>



<?php echo get_paging(G5_IS_MOBILE ? $config['cf_mobile_pages'] : $config['cf_write_pages'], $page, $total_page, "{$_SERVER['SCRIPT_NAME']}?$qstr&amp;page=", "&amp;qstn_grop_cd=$qstn_grop_cd"); ?>

<?php
include_once (G5_PATH.'/tail.php');
?>
