import { AppBar, Box, Button, Toolbar, Typography } from "@mui/material";
import { Link } from "react-router-dom";

export function Header() {
    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static" color="transparent">
                <Toolbar variant="dense">
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Tests
                    </Typography>
                    <Button color="inherit" >
                        <Link to="/login" >Login</Link>
                    </Button>
                    <Button color="inherit" >
                        <Link to="/register" >Register</Link>
                    </Button>
                </Toolbar>
            </AppBar>
        </Box>
    );
}
