<h1 align="center">
GoodPass -- 开发中的密码管家
<h1 align="center">
  <img src="https://github.com/GeorgeDong32/GoodPass/blob/resource/Title%20Photo/GoodPass2.0T.png" alt="GoodPass" width="400">
</h1>
  <p align="center">
    <a href="/Readmes/README_tcn.md">繁體中文</a>
    ·
    <a href="/README.md">English</a>
</p>

## ©️ 版权
> 由GeorgeDong32开发并发布 
>
> <Copyright (c) GeorgeDong32(Github). 保留所有权利.>
## 🎤 简介
GoodPass是一个开发中的密码管家。它目前完全在本地运行，在本地加密和解密您的数据，无需担心联网所带来的安全风险。
## 📦 发行版本  [<img src="https://img.shields.io/badge/GoodPass-Release-34558b" alt="Release">](https://github.com/GeorgeDong32/GoodPass/releases)
您可以在[Releases](https://github.com/GeorgeDong32/GoodPass/releases)界面获取应用,我对您的使用表示由衷地感谢.

[![Release Version](https://img.shields.io/github/v/release/GeorgeDong32/GoodPass)](https://github.com/GeorgeDong32/GoodPass/releases/latest)

## 💬 反馈  [<img src="https://img.shields.io/badge/GoodPass-Feedback-939597" alt="Issue">](https://github.com/GeorgeDong32/GoodPass/issues)
作为一个开发中的应用，我十分希望听到使用者的反馈，您可以将你的建议或意见发送到 `georgedong32@foxmail.com` 或点击上方的Feedback按钮进入Issue板块提交反馈。

## 🔐关于安全
#### 🏗️ GPSES--GoodPass对称加密系统正在开发中，目前为1.0版本
#### 🔏 GPHES(GoodPass哈希加密验证系统)是基于SHA256的 `salted hash` 加密验证系统，用于校验主密码
#### 🔒 加密系统支持列表
##### 密码存放系统
| 版本 | GP_cryption        | GPSES 1.0         |
| ------- | ------------------ | ----------------- |
| 2.2.0   | :white_check_mark: | :white_check_mark:|
| 2.1.0   | :white_check_mark: | :white_check_mark:|
| 2.0.5   | :white_check_mark: | :x:               |
| 2.0.2   | :white_check_mark: | :x:               |
| 2.0.1   | :white_check_mark: | :x:               |
| 2.0.0   | :white_check_mark: | :x:               |
| 1.8.0   | :white_check_mark: | :x:               |

##### 主密码校验系统 
| 版本 | MD5                | SHA256             | GPHES              |
| ------- | ------------------ | ------------------ | ------------------ |
| 2.2.0   | :x:                | :x:                | :white_check_mark: |
| 2.1.0   | :x:                | :x:                | :white_check_mark: |
| 2.0.5   | :x:                | :x:                | :white_check_mark: |
| 2.0.2   | :x:                | :white_check_mark: | :x:                |
| 2.0.1   | :white_check_mark: | :x:                | :x:                |
| 2.0.0   | :x:                | :x:                | :x:                |
| 1.8.0   | :x:                | :x:                | :x:                |

## 📈 开发计划
🚧 GPHES(GoodPass哈希加密验证系统)与GPSES(GoodPass对称加密系统)，二者都是TEA(二重加密算法)，他们将不断完善以保护您的数据安全

🏗️ 基于QT的图形界面正在开发中，预计将于7月在v3.0.0问世
## 🛡 文件安全性
发行文件的SHA256值已被记录在下列文件中，请前往查阅
  
[<img src="https://img.shields.io/badge/GoodPass-File SHA256-34558b" alt="File SHA256">](https://github.com/GeorgeDong32/GoodPass/blob/main/File_SHA256.md)
