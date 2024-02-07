# Server Quán Nét
## _Created by HoangKevin_

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Server của quán net 4.0. Chạy trên máy chính của quán như một server.
Sử dụng WebSocket và ngôn ngữ C#.

## Chức năng

- Xử lý, quản lý thông tin người dùng.
- Quản lý tài khoản bằng SQL server.
- Bảo mật thông tin, chống người dùng hack thời gian, tiền, ...
## Cách thức hoạt động
- Sử dụng websocket trao đổi với client, tăng tốc độ trao đổi thông tin, bảo mật thông tin một cách tốt nhất
- Khi client gửi thông tin đến server, server sẽ xử lý theo những data mà client gửi lên, rồi trả ngược lại client.
- Kick các client khi đăng nhập trùng một tài khoản.
- To be continue(Sẽ cập nhật nếu có chức năng gì mới heheehhe)

## _Chú ý: Project này được làm bởi sv năm nhất, nên chắc chắc sai sót là rất nhiều, nhưng tớ sẽ cố gắng cải thiện, nâng cấp project này!!_

## Contributor

Cảm ơn những dev đã giúp đỡ tớ hoàn thành project này:

- [Deo co ai](https://www.youtube.com/watch?v=dQw4w9WgXcQ) - Den bay h deo co ai muon giup toi ca, nhưng nếu bạn muốn đóng góp, đừng ngần ngại đóng góp!!!


## Installation

Yêu cầu cài:
- SDK .NET: (https://dotnet.microsoft.com/en-us/download)
- Visual Studio Code (Hoặc Visual Studio nếu bạn vip pro!): (https://code.visualstudio.com/download)

Mở một folder, sử dụng 2 cách sau đây:
- Nếu bạn đã cài git, mở terminal lên và dùng lệnh:
```
git clone https://github.com/hoangcoderr/Server-QuanNet4_0.git
```
- Nếu chưa cài thì bạn hãy cài đi, nó tiện vl!!

Sau khi đã clone xong, hãy chạy:

```
dotnet run
```

## Library

Các thư viện tớ đã dùng cho project này:

| Library | README |
| ------ | ------ |
| WebSocketSharp | [https://github.com/sta/websocket-sharp/blob/master/README.md][PlDb] |
| MySqlClient | [https://github.com/PyMySQL/mysqlclient/blob/main/README.md][PlGh] |


## License

Nah, ít's open source, i love you guys <3
