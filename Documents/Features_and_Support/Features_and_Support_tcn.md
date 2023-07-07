# 功能與支持

## 🔐安全性

### 🏗️ GPSES

**GPSES** 是一套对称加密系統，**GoodPass**將其用於加密你的密碼，阻止密碼泄露

### 🔏 GPHES(GoodPass 哈希加密驗證系統)

**GPHES**是一個`salted hash`哈希加密系統，**GoodPass**將其用於驗證主密碼，防止其他用戶訪問你的數據

## 支持列表
### :desktop_computer: 界面

| Version | CLI界面 |      GUI界面       |
| :-----: | :-----: | :----------------: |
|   v2    |    ✅    |        :x:         |
|   v3    |   :x:   | :white_check_mark: |

### 🔒 加密系統支持列表

#### 密碼存儲

| Version |    GP_cryption     |     GPSES 1.0      |     數據自升級     |
| :-----: | :----------------: | :----------------: | :----------------: |
|   v2    | :white_check_mark: | :white_check_mark: | :white_check_mark: |
|   v3    |        :x:         | :white_check_mark: |        :x:         |
#### 主密碼校驗

| Version | MD5  | SHA256 |       GPHES        |
| :-----: | :--: | :----: | :----------------: |
|   v2    | :x:  |  :x:   | :white_check_mark: |
|   v3    | :x:  |  :x:   | :white_check_mark: |

#### 维护支持

|   Version    |      漏洞修復      |      安全更新      |      功能更新      |
| :----------: | :----------------: | :----------------: | :----------------: |
| v2 (< 2.7.0) |        :x:         |        :x:         |        :x:         |
| v2 (> 2.7.0) | :white_check_mark: | :white_check_mark: | :white_check_mark: |
|      v3      | :white_check_mark: | :white_check_mark: | :white_check_mark: |