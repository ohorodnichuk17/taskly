import z from "zod";
export type EmailValidationType = z.infer<typeof EmailValidationShema>;
export declare const EmailValidationShema: z.ZodObject<{
    email: z.ZodString;
}, "strip", z.ZodTypeAny, {
    email: string;
}, {
    email: string;
}>;
export type EmailVerificationType = z.infer<typeof EmailVerificationShema>;
export declare const EmailVerificationShema: z.ZodObject<{
    email: z.ZodString;
    code: z.ZodArray<z.ZodString, "many">;
}, "strip", z.ZodTypeAny, {
    email: string;
    code: string[];
}, {
    email: string;
    code: string[];
}>;
export type PasswordValidationType = z.infer<typeof PasswordValidationShema>;
export declare const PasswordValidationShema: z.ZodEffects<z.ZodObject<{
    password: z.ZodString;
    confirmPassword: z.ZodString;
}, "strip", z.ZodTypeAny, {
    password: string;
    confirmPassword: string;
}, {
    password: string;
    confirmPassword: string;
}>, {
    password: string;
    confirmPassword: string;
}, {
    password: string;
    confirmPassword: string;
}>;
export type LoginType = z.infer<typeof LoginShema>;
export declare const LoginShema: z.ZodObject<{
    email: z.ZodString;
    password: z.ZodString;
    rememberMe: z.ZodDefault<z.ZodBoolean>;
}, "strip", z.ZodTypeAny, {
    email: string;
    password: string;
    rememberMe: boolean;
}, {
    email: string;
    password: string;
    rememberMe?: boolean | undefined;
}>;
export type NewCardType = z.infer<typeof NewCardShema>;
export declare const NewCardShema: z.ZodObject<{
    task: z.ZodString;
    deadline: z.ZodDate;
    isPublicCard: z.ZodBoolean;
}, "strip", z.ZodTypeAny, {
    task: string;
    deadline: Date;
    isPublicCard: boolean;
}, {
    task: string;
    deadline: Date;
    isPublicCard: boolean;
}>;
export type GenerateCardsWithAIType = z.infer<typeof GenerateCardsWithAIShema>;
export declare const GenerateCardsWithAIShema: z.ZodObject<{
    description: z.ZodString;
}, "strip", z.ZodTypeAny, {
    description: string;
}, {
    description: string;
}>;
export type CreateBoardType = z.infer<typeof CreateBoardShema>;
export declare const CreateBoardShema: z.ZodObject<{
    name: z.ZodString;
    tag: z.ZodEffects<z.ZodNullable<z.ZodString>, string | null, string | null>;
    boadrTemplateId: z.ZodString;
}, "strip", z.ZodTypeAny, {
    name: string;
    tag: string | null;
    boadrTemplateId: string;
}, {
    name: string;
    tag: string | null;
    boadrTemplateId: string;
}>;

