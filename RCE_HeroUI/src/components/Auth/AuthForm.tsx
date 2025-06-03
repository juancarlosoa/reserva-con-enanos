import { useState } from "react";
import { Form, Input, Button } from '@heroui/react';

type AuthFormProps = {
    mode: 'login' | 'register';
    onSubmit: (data: Record<string, FormDataEntryValue>) => void;
}
export default function AuthForm({ mode, onSubmit }: AuthFormProps) {
    const [action, setAction] = useState("");

    return (
        <Form
            className="w-full max-w-xs flex flex-col gap-4"
            onReset={() => setAction("reset")}
            onSubmit={(e) => {
                e.preventDefault();
                const formData = new FormData(e.currentTarget);
                const data = Object.fromEntries(formData);
                onSubmit(data);
            }}
        >
            <Input
                isRequired
                errorMessage="Please enter a valid email"
                label="Email"
                labelPlacement="outside"
                name="email"
                placeholder="Enter your email"
                type="email"
            />
            <Input
                isRequired
                label="Password"
                name="password"
                placeholder="Enter your password"
                minLength={6}
                type="password"
            />
            {mode == "register" && (
                <Input
                    isRequired
                    minLength={6}
                    label="Confirm Password"
                    name="confirmPassword"
                    placeholder="Repeat your password"
                    type="password"
                />
            )}
            <div className="flex gap-2">
                <Button color="primary" type="submit">
                    {mode == 'login' ? 'Login' : 'Register'}
                </Button>
                <Button type="reset" variant="flat">
                    Reset
                </Button>
            </div>
            {action && (
                <div className="text-small text-default-500">
                    Action: <code>{action}</code>
                </div>
            )}
        </Form>
    );

} 