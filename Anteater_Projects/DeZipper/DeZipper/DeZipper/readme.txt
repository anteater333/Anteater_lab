﻿ 			############################
			######### DeZipper #########
			############################

-----------------
 개요
-----------------
DeZipper는 zip 파일의 파일 리스트를 읽은 다음, 원하는 폴더에서 삭제 작업을 수행하는 프로그램입니다.

-----------------
 사용
-----------------
 = 기본 사용법 =
DeZipper.exe [source_zip]
 : [source_zip] 에서 파일 리스트를 읽어 출력합니다.
DeZipper.exe [source_zip] [target_dir]
 : [source_zip] 에서 파일 리스트를 읽어서 [target_dir] 에서 삭제합니다.
DeZipper.exe [source_zip] [target_dir] [options]
 : [options] 에 따라 옵션을 설정할 수 있습니다. [options] 는 띄어쓰기로 구분됩니다.

 = 옵션 =
 (옵션은 파일 삭제시에만 사용 가능합니다.)
-s, -silence
 : 에러 메세지를 제외한 나머지 메세지를 출력하지 않습니다.
-e, -empty
 : 파일 삭제 후 파일 리스트에 존재하는 폴더 중 빈 폴더를 삭제합니다.
-z, -zip
 : 파일 삭제 후 사용된 zip 파일을 삭제합니다.
-r, -recycle
 : 파일을 삭제하는 대신 휴지통으로 보냅니다.
   (주의!) 아직 구현되지 않은 기능입니다.
-ex [file], -exclude [file]
 : 삭제할 파일 리스트에서 [file] 을 제외합니다. 여러 파일을 제외할 경우 띄어쓰기로 구분합니다.
   다중 옵션 사용 시 해당 옵션은 마지막에 위치하는것을 권장합니다.
   제외할 파일을 찾지 못한 경우 삭제는 진행되지 않습니다.
-h, -help
 : readme.txt 파일을 출력합니다.

-----------------
 Credit
-----------------
 Auth : anteater333 (@ Github.com)
 E-mail : zx1056@naver.com

-----------------
 Change Log
-----------------
2017-MM-DD, v0.1
 : DeZipper Release