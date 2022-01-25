import React, { Component } from "react";
import { Redirect } from "react-router";
import { deleteCookie } from "./../Utils";

export class Logout extends Component {

    componentDidMount() {
        deleteCookie("username");
    }

    render() {
        return <Redirect to="/Login" />;
    }
}

export default Logout;
