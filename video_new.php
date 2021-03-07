 <html><head>
    <title>Timvi</title>
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
	<meta charset='utf-8' />
	<script type="text/javascript" src="../script/jquery-2.0.3.min.js"></script>
	<script type="text/javascript" src="coherent.js"></script>
	<style>
  body
    {
        margin:0px;
        padding:0px;
        background-color:Black;
    }
    table
    {
        width:100%;
        height:100%;

    }
    #source
    {
        border-bottom-style:solid;
        border-bottom-width: 0px;

        border-left-style:solid;
        border-left-width: 5px;

        border-right-style:solid;
        border-right-width: 5px;
    }
</style>	
<script src="http://www.youtube.com/player_api"></script>
</head>

<body> 
 
<table border="0 cellspacing="0" cellpadding="0" >
<tr>
<td id="source">
    <div id="player"></div>

</td>
</tr>
</table>

<?php
error_reporting(0);

	function get_youtube_id($url)
	{
		$video_id = $url;

		if (strpos($url, 'youtu.be'))
		{
			$video_id = split('/', $url);	
			$video_id  = array_pop($video_id);
		}
		
		if (strpos($url, 'watch?v='))
		{
			$video_id = split('v=', $url);	
			$video_id  = array_pop($video_id);
		}
	 
	return $video_id;
	}
 
?>
   

  <script type="text/javascript">


  var player;
      function onYouTubeIframeAPIReady() {
        player = new YT.Player('player', {
          height: '511',
          width: '1023',
		  
  <?php echo 'videoId: "'.get_youtube_id($_GET['id']).'",'?>
  
		    playerVars: { 'controls': 0 , 'showinfo':0, 'modestbranding':0, 'rel':0},
          events: {
            'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange
          }
        });
      }

      // 4. The API will call this function when the video player is ready.
      function onPlayerReady(event) {
		<?php if ($_GET['play'] == 'true' ) echo 'event.target.playVideo();' ;?>
      }

      // 5. The API calls this function when the player's state changes.
      //    The function indicates that when playing a video (state=1),
      //    the player should play for six seconds and then stop.
      var done = false;
      function onPlayerStateChange(event) {
        if (event.data == YT.PlayerState.PLAYING && !done) {
          setTimeout(stopVideo, 6000);
          done = true;
        }
      }
 
function play() {
  
    player.playVideo();
 
}	

function stop() {
 
    player.stopVideo();
  
}


function pause() {

    player.pauseVideo();

}

function fastForward() // Перемотка вперед на 2 сек.
	{
		var position =  player.getCurrentTime();
		player.seekTo(position + 2, 1);
	}

function rewind() // Перемотка назад на 2 сек.
	{
		var position =  player.getCurrentTime();
		player.seekTo(position - 2, 1);
	}
	
function getDuration() // Возвращает длительность в секундах видео
	{
		return player.getDuration(); 
	}
	
function getPositionSimple() // Возвращает время в секундах, прошедшее с начала воспроизведения видео.
	{
		return player.getCurrentTime();
	}

function getPosition()
	{
		var duration = player.getDuration(); 
		return player.getCurrentTime()/ duration;
	}
	
function setPositionSimple(value)
	{
		
			player.seekTo(value, 1);
		
	}

function setPosition(value)
	{
		var duration = player.getDuration(); 
		player.seekTo(duration * value);
	}
	
	
function setMute(value)
	{
		
		  player.setVolume(value);
	 
	}
	
function getMute()
	{
			
		return  player.getVolume();
	 	
	}
	
engine.on('getVideoPos', function () {
 engine.call("getPositionUnity", getPosition());
});


engine.on('getVolume', function () {
 engine.call("getVolumeUnity", getMute());
});
	
  </script>
</body>	
  </html> 
  