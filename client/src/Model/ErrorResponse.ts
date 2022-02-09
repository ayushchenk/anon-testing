export class ErrorResponse {
    public constructor(
        public readonly error: string,
        public readonly errors: { [key: string]: string }
    ) { }
}