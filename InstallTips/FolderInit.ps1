# GoodPass Folder Init by GoodPass Developer
# 创建新文件夹
New-Item -Path $env:LOCALAPPDATA -Name GoodPass -ItemType Directory
# 创建新文件
New-Item -Path "$env:LOCALAPPDATA\GoodPass" -Name GoodPassData.csv -ItemType File
# end GoodPass Folder Init by GoodPass Developer