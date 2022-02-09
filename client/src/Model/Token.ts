export class Token {
    public constructor(
        public readonly value: string,
        public readonly userId: string,
        public readonly expiresOn: Date
    ) { }
}