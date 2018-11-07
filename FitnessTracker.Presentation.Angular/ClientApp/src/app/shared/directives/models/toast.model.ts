export class Toast {
    message: string;
    timeout: number;
    color: string;

    constructor(message: string, timeout: number, color: string) {
        this.message = message;
        this.timeout = timeout;
        this.color = color;
    }
}
