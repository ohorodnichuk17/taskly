import z from "zod";

export type EmailValidationType = z.infer<typeof EmailValidationShema>;
export const EmailValidationShema = z.object({
    email: z.string().email()
});

export type EmailVerificationType = z.infer<typeof EmailVerificationShema>;
export const EmailVerificationShema = z.object({
    email: z.string().email(),
    code: z.array(
        z.string().regex(/[0-9]/, "Each field must have one number").length(1)

    ).length(7)
});

export type PasswordValidationType = z.infer<typeof PasswordValidationShema>;
export const PasswordValidationShema = z.object({
    password: z.string().min(10, "Password must contain at least 10 character(s)"),
    confirmPassword: z.string()
}).superRefine((obj, ctx) => {
    if (obj.password !== obj.confirmPassword) {
        ctx.addIssue({
            code: z.ZodIssueCode.custom,
            message: "Password and Confirm Password must be equal",
            path: ["confirmPassword"]
        });
    }
})

export type LoginType = z.infer<typeof LoginShema>;
export const LoginShema = z.object({
    email: z.string().email(),
    password: z.string().min(10, "Password must contain at least 10 character(s)"),
    rememberMe: z.boolean().default(false)
})
