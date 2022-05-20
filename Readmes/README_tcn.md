<h1 align="center">
GoodPass -- 開發中的密碼管家
<h1 align="center">
  <img src="https://github.com/GeorgeDong32/GoodPass/blob/resource/Title%20Photo/GoodPass2.0T.png" alt="GoodPass" width="400">
</h1>
  <p align="center">
    <a href="/README.md">English</a>
    ·
    <a href="/Readmes/README_scn.md">简体中文</a>
</p>

## ©️ 版權
> 由GeorgeDong32開發
>
> <Copyright (c) GeorgeDong32(Github). All rights reserved.>
## 🎤 簡介
GoodPass 係一個開發中的密碼管家。它目前完全在本地運行，在本地加密和解密你的數據，你無需擔心聯網所帶來的安全風險
## 📦 發行版本  [<img src="https://img.shields.io/badge/GoodPass-Release-34558b" alt="Release">](https://github.com/GeorgeDong32/GoodPass/releases)
你可以在[Releases](https://github.com/GeorgeDong32/GoodPass/releases)界面獲取應用, 感謝你的使用

[![Release Version](https://img.shields.io/github/v/release/GeorgeDong32/GoodPass)](https://github.com/GeorgeDong32/GoodPass/releases/latest)

## 💬 反饋  [<img src="https://img.shields.io/badge/GoodPass-Feedback-939597" alt="Issue">](https://github.com/GeorgeDong32/GoodPass/issues)
我非常希望能夠聽到你的聲音，你可以將建議和意見發送到`georgedong32@foxmail.com`或者點擊上方`feedback`按鈕提交反饋

## 🔐 安全性
#### 🏗️ GPSES--GoodPass對稱加密系統正在開發中，為目前爲1.0版本
#### 🔏 GPHES(GoodPass哈希加密驗證系統)是基於SHA256的 `salted hash` 加密驗證系統,用於校驗主密碼
#### 🔒 加密系統支持表
##### 密碼存儲
| 版本 | GP_cryption        | GPSES 1.0         |
| ------- | ------------------ | ----------------- |
| 2.2.0   | :white_check_mark: | :white_check_mark:|
| 2.1.0   | :white_check_mark: | :white_check_mark:|
| 2.0.5   | :white_check_mark: | :x:               |
| 2.0.2   | :white_check_mark: | :x:               |
| 2.0.1   | :white_check_mark: | :x:               |
| 2.0.0   | :white_check_mark: | :x:               |
| 1.8.0   | :white_check_mark: | :x:               |

##### 主密碼校驗 
| 版本 | MD5                | SHA256             | GPHES              |
| ------- | ------------------ | ------------------ | ------------------ |
| 2.2.0   | :x:                | :x:                | :white_check_mark: |
| 2.1.0   | :x:                | :x:                | :white_check_mark: |
| 2.0.5   | :x:                | :x:                | :white_check_mark: |
| 2.0.2   | :x:                | :white_check_mark: | :x:                |
| 2.0.1   | :white_check_mark: | :x:                | :x:                |
| 2.0.0   | :x:                | :x:                | :x:                |
| 1.8.0   | :x:                | :x:                | :x:                |

## 📈 開發計劃
🚧 GPHES(GoodPass哈希加密驗證系統)与GPSES(GoodPass對稱加密系統)，兩者都為TEA(二重加密算法)，它們將不斷完善以保護你的數據安全

🏗️ 基於QT的圖形界面正在開發,預計將於7月於v3.0.0問世
## 🛡發行文件安全性
發行文件的SHA256值記錄于下方文檔,請前往查閲
  
[<img src="https://img.shields.io/badge/GoodPass-File SHA256-34558b" alt="File SHA256">](https://github.com/GeorgeDong32/GoodPass/blob/main/File_SHA256.md)
