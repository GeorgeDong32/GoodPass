# Features and Support
## 🔐Security

### 🏗️ GPSES
GPSES is a symmetric encryption system. GoodPass uses it to encrypt your passwords and keep them away form leaking out.
### 🔏 GPHES(GoodPass Hash Encryption System) 
GPHES is a salted hash encryption system. It uses your main key to process a salt to enhance SHA256. It is used to verify the input mainkey and verify user identity.

## Support Lists
### 🔒 Encryption System Support
#### PassWord Storage
| Version | GP_cryption        | GPSES 1.0         |
| ------- | ------------------ | ----------------- |
| 2.2.0   | :white_check_mark: | :white_check_mark:|
| 2.1.0   | :white_check_mark: | :white_check_mark:|
| 2.0.5   | :white_check_mark: | :x:               |
| 2.0.2   | :white_check_mark: | :x:               |
| 2.0.1   | :white_check_mark: | :x:               |
| 2.0.0   | :white_check_mark: | :x:               |
| 1.8.0   | :white_check_mark: | :x:               |

#### Master password verification 
| Version | MD5                | SHA256             | GPHES              |
| ------- | ------------------ | ------------------ | ------------------ |
| 2.2.0   | :x:                | :x:                | :white_check_mark: |
| 2.1.0   | :x:                | :x:                | :white_check_mark: |
| 2.0.5   | :x:                | :x:                | :white_check_mark: |
| 2.0.2   | :x:                | :white_check_mark: | :x:                |
| 2.0.1   | :white_check_mark: | :x:                | :x:                |
| 2.0.0   | :x:                | :x:                | :x:                |
| 1.8.0   | :x:                | :x:                | :x:                |
