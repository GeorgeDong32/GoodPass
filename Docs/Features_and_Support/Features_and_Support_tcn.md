# 功能與支持

## 🔐安全性

### 🏗️ GPSES

**GPSES** 是一套对称加密系統，**GoodPass**將其用於加密你的密碼，阻止密碼泄露

### 🔏 GPHES(GoodPass 哈希加密驗證系統)

**GPHES**是一個`salted hash`哈希加密系統，**GoodPass**將其用於驗證主密碼，防止其他用戶訪問你的數據

## 支持列表

### 🔒 加密系統支持列表

#### 密碼存儲

| Version | GP_cryption        | GPSES 1.0          |
| ------- | ------------------ | ------------------ |
| 2.2.0   | :white_check_mark: | :white_check_mark: |
| 2.1.0   | :white_check_mark: | :white_check_mark: |
| 2.0.5   | :white_check_mark: | :x:                |
| 2.0.2   | :white_check_mark: | :x:                |
| 2.0.1   | :white_check_mark: | :x:                |
| 2.0.0   | :white_check_mark: | :x:                |
| 1.8.0   | :white_check_mark: | :x:                |

#### 主密碼校驗

| Version | MD5                | SHA256             | GPHES              |
| ------- | ------------------ | ------------------ | ------------------ |
| 2.2.0   | :x:                | :x:                | :white_check_mark: |
| 2.1.0   | :x:                | :x:                | :white_check_mark: |
| 2.0.5   | :x:                | :x:                | :white_check_mark: |
| 2.0.2   | :x:                | :white_check_mark: | :x:                |
| 2.0.1   | :white_check_mark: | :x:                | :x:                |
| 2.0.0   | :x:                | :x:                | :x:                |
| 1.8.0   | :x:                | :x:                | :x:                |