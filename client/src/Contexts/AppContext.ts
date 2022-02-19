import React from "react";

export interface ApplicationContext {
    isAuthed: boolean;
}

const AppContext = React.createContext<ApplicationContext>({
    isAuthed: false
});

export default AppContext;