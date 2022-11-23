# 功能与支持

## 🔐安全性

### 🏗️ GPSES

**GPSES** 是一套对称加密系统，**GoodPass**将其用于加密你的密码，防止密码泄露

### 🔏 GPHES(GoodPass 哈希加密验证系统)

**GPHES**是一个`salted hash`哈希加密系统，**GoodPass**将其用于验证主密码，防止其他用户访问你的数据

## 支持列表

### :desktop_computer: 界面

| Version | CLI界面 |      GUI界面       |
| :-----: | :-----: | :----------------: |
|   v2    |    ✅    |        :x:         |
|   v3    |   :x:   | :white_check_mark: |

### 🔒 加密系统支持列表

#### 密码存储

| Version |    GP_cryption     |     GPSES 1.0      |     数据自升级     |
| :-----: | :----------------: | :----------------: | :----------------: |
|   v2    | :white_check_mark: | :white_check_mark: | :white_check_mark: |
|   v3    |        :x:         | :white_check_mark: |        :x:         |

#### 主密码校验

| Version | MD5  | SHA256 |       GPHES        |
| :-----: | :--: | :----: | :----------------: |
|   v2    | :x:  |  :x:   | :white_check_mark: |
|   v3    | :x:  |  :x:   | :white_check_mark: |

#### 维护支持

|   Version    |      漏洞修复      |      安全更新      |      功能更新      |
| :----------: | :----------------: | :----------------: | :----------------: |
| v2 (< 2.7.0) |        :x:         |        :x:         |        :x:         |
| v2 (> 2.7.0) | :white_check_mark: | :white_check_mark: | :white_check_mark: |
|      v3      | :white_check_mark: | :white_check_mark: | :white_check_mark: |