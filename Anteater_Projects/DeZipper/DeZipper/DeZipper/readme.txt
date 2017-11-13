 = 기본 사용법 =
DeZipper.exe [source_zip]
 : [source_zip] 에서 파일 리스트를 읽어옴.
DeZipper.exe [source_zip] [target_dir]
 : [source_zip] 에서 파일 리스트를 읽어서 [target_dir] 에서 삭제.
DeZipper.exe [source_zip] [target_dir] [options]
 : [options] 에 따라 옵션 설정, [options] 는 띄어쓰기로 구분.
 = 옵션 =
 (옵션은 파일 삭제시에만 사용 가능)
-s, -silence
 : 출력 없음.
-e, -empty
 : 삭제 후 빈 폴더 삭제
-z, -zip
 : 삭제 후 원본 ZIP 파일 삭제
-r, -recycle	[!! 미구현 !!]
 : 휴지통으로 보냄
-ex [file], -exclude [file]
 : 파일 리스트에서 [file] 을 제외. 여러 파일을 제외할 경우 띄어쓰기로 구분. 다중 옵션 사용 시 해당 옵션은 마지막에 위치하는것을 권장.
-h, -help
 : readme.txt 파일을 출력합니다.