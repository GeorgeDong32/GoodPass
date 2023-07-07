# Features and Support
## 🔐Security

### 🏗️ GPSES
GPSES is a symmetric encryption system. GoodPass uses it to encrypt your passwords and keep them away form leaking out.
### 🔏 GPHES(GoodPass Hash Encryption System) 
GPHES is a salted hash encryption system. It uses your main key to process a salt to enhance SHA256. It is used to verify the input mainkey and verify user identity.

## Support Lists

### :desktop_computer: Interface

| Version | CLI界面 |      GUI界面       |
| :-----: | :-----: | :----------------: |
|   v2    |    ✅    |        :x:         |
|   v3    |   :x:   | :white_check_mark: |

### 🔒 Encryption System Support
#### PassWord Storage

| Version |    GP_cryption     |     GPSES 1.0      |  Data Self Update  |
| :-----: | :----------------: | :----------------: | :----------------: |
|   v2    | :white_check_mark: | :white_check_mark: | :white_check_mark: |
|   v3    |        :x:         | :white_check_mark: |        :x:         |

#### Master password verification 
| Version | MD5  | SHA256 |       GPHES        |
| :-----: | :--: | :----: | :----------------: |
|   v2    | :x:  |  :x:   | :white_check_mark: |
|   v3    | :x:  |  :x:   | :white_check_mark: |

### Maintenance support

|   Version    |      Bug fix       |  Security updates  |  Feature updates   |
| :----------: | :----------------: | :----------------: | :----------------: |
| v2 (< 2.7.0) |        :x:         |        :x:         |        :x:         |
| v2 (> 2.7.0) | :white_check_mark: | :white_check_mark: | :white_check_mark: |
|      v3      | :white_check_mark: | :white_check_mark: | :white_check_mark: |
