# Sơ đồ tư duy Docker cơ bản

```mermaid
graph TD
    subgraph Build & Deploy
        Dockerfile["Dockerfile\n- Viết hướng dẫn build image"]
        Image["Image\n- Đóng gói app + phụ thuộc"]
        Container["Container\n- Chạy thực tế"]
        Dockerfile --> Image
        Image --> Container
    end

    subgraph Storage & Data
        Volume["Volume\n- Lưu trữ dữ liệu ngoài container"]
        Container -- "Đọc/Ghi dữ liệu" --> Volume
    end

    subgraph Network & Communication
        Network["Network\n- Kết nối các container"]
        Container -- "Giao tiếp" --> Network
    end

    subgraph Access
        Port["Port mapping\n- Chuyển tiếp cổng host <-> container"]
        Host["Host (Máy tính của bạn)"]
        Host -- "Truy cập qua port" --> Port
        Port --> Container
    end

    subgraph Orchestration
        Compose["Docker Compose\n- Quản lý nhiều container, network, volume"]
        Compose -- "Tạo & quản lý" --> Container
        Compose -- "Tạo & quản lý" --> Network
        Compose -- "Tạo & quản lý" --> Volume
    end
```

---

## Giải thích từng phần

- **Dockerfile**: Viết hướng dẫn build image cho app.
- **Image**: Được build từ Dockerfile, chứa app và các phụ thuộc.
- **Container**: Instance của image, chạy thực tế.
- **Volume**: Lưu trữ dữ liệu ngoài container, tránh mất khi container bị xóa.
- **Network**: Kết nối các container, giúp chúng giao tiếp với nhau.
- **Port mapping**: Chuyển tiếp cổng từ máy host vào container để truy cập dịch vụ.
- **Docker Compose**: Quản lý nhiều container, network, volume cùng lúc, giúp triển khai hệ thống phức tạp dễ dàng.

Bạn có thể copy sơ đồ Mermaid này vào [Mermaid Live Editor](https://mermaid-js.github.io/mermaid-live-editor/) hoặc dùng extension Mermaid trong VS Code để xem hình ảnh trực quan.
