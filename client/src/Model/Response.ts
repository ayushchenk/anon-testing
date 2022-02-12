interface Keyable {
    [key: string]: string;
}

export class Response<T> {
    private constructor(
        public readonly ok: boolean,
        public readonly error?: string,
        public readonly errors?: Keyable,
        public readonly value?: T
    ) { }

    public static ok<T>(value: T): Response<T> {
        return new Response(true, undefined, undefined, value);
    }

    public static fail(error?: string, errors?: Keyable): Response<undefined> {
        return new Response(false, error, errors, undefined);
    }
}