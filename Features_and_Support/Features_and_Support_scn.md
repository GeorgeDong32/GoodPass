# 功能与支持



## 🔐安全性

### 🏗️ GPSES

**GPSES** 是一套对称加密系统，**GoodPass**将其用于加密你的密码，防止密码泄露

### 🔏 GPHES(GoodPass 哈希加密验证系统)

**GPHES**是一个`salted hash`哈希加密系统，**GoodPass**将其用于验证主密码，防止其他用户访问你的数据

## 支持列表

### 🔒 加密系统支持列表

#### 密码存储

| Version | GP_cryption        | GPSES 1.0          |
| ------- | ------------------ | ------------------ |
| 2.2.0   | :white_check_mark: | :white_check_mark: |
| 2.1.0   | :white_check_mark: | :white_check_mark: |
| 2.0.5   | :white_check_mark: | :x:                |
| 2.0.2   | :white_check_mark: | :x:                |
| 2.0.1   | :white_check_mark: | :x:                |
| 2.0.0   | :white_check_mark: | :x:                |
| 1.8.0   | :white_check_mark: | :x:                |

#### 主密码校验

| Version | MD5                | SHA256             | GPHES              |
| ------- | ------------------ | ------------------ | ------------------ |
| 2.2.0   | :x:                | :x:                | :white_check_mark: |
| 2.1.0   | :x:                | :x:                | :white_check_mark: |
| 2.0.5   | :x:                | :x:                | :white_check_mark: |
| 2.0.2   | :x:                | :white_check_mark: | :x:                |
| 2.0.1   | :white_check_mark: | :x:                | :x:                |
| 2.0.0   | :x:                | :x:                | :x:                |
| 1.8.0   | :x:                | :x:                | :x:                |