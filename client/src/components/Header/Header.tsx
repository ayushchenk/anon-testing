import { AppBar, Box, Button, Toolbar, Typography } from "@mui/material";
import React from "react";
import { Link } from "react-router-dom";
import AppContext from "../../Contexts/AppContext";

interface HeaderState {
    loginRedirect: boolean;
    registerRedirect: boolean;
}

export class Header extends React.Component<{}, HeaderState> {
    public static contextType = AppContext;
    public context!: React.ContextType<typeof AppContext>;

    public constructor(props: {}) {
        super(props);

        this.state = {
            loginRedirect: false,
            registerRedirect: false
        };
    }

    public render(): React.ReactNode {
        return (
            <Box sx={{ flexGrow: 1 }}>
                <AppBar position="static" color="transparent">
                    <Toolbar variant="dense">
                        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                            Tests
                        </Typography>
                        <Button color="inherit" onClick={() => this.setState({ loginRedirect: true })}>
                            <Link to="/login" >Login</Link>
                        </Button>
                        <Button color="inherit" onClick={() => this.setState({ registerRedirect: true })}>
                            <Link to="/register" >Register</Link>
                        </Button>
                    </Toolbar>
                </AppBar>
            </Box>
        );
    }
}