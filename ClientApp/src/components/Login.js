import React, { Component } from "react";
import { Redirect } from "react-router";
import "./../css/Login.css";
import { checkIfLoggedIn, setCookie } from "./../Utils";

export class Login extends Component {
    state = {logged: false, showError : false };

    handleLoginClick = async (e) => {
        e.preventDefault();
        const login = document.getElementById("loginInput").value;
        const pass = document.getElementById("passwordInput").value;

        if (login && pass) {
            const response = await fetch(`User/Authenticate?login=${login}&password=${pass}`);
            if (response.status == "200") {
                setCookie("username", login, 10);
                this.setState({ logged: true, showError : false});
            } else {
                this.setState({ logged: false, showError : true });

            }
        } else {
            this.setState({ logged: false, showError : true });

        }
    };

    render() {

        if (checkIfLoggedIn()) {
            return <Redirect to="/" />;
        }

        const img = require("./../resources/user-login-icon-vector-21078913.jpg");

        return (
            <div>

                <div class="alert alert-danger alert-dismissible fade show" role="alert" style={{display: this.state.showError ? "" : "none"}}>
                    Invalid username or password
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <form className="container LoginForm">
                    <div className="container Logodiv">
                        <img className="LogoImg" src={img}></img>
                    </div>
                    <div className="mb-3">
                        <label for="loginInput" className="form-label">
                            Email address
                        </label>
                        <input
                            id="loginInput"
                            type="email"
                            className="form-control"
                            aria-describedby="emailHelp"
                        />
                        <div id="emailHelp" className="form-text">
                            We'll never share your email with anyone else.
                        </div>
                    </div>
                    <div className="mb-3">
                        <label for="passwordInput" className="form-label">
                            Password
                        </label>
                        <input
                            id="passwordInput"
                            type="password"
                            className="form-control"
                        />
                    </div>
                    <div className="mb-3 form-check">
                        <input
                            type="checkbox"
                            className="form-check-input"
                            id="exampleCheck1"
                        />
                        <label className="form-check-label" for="exampleCheck1">
                            Remember me
                        </label>
                    </div>
                    <button
                        onClick={this.handleLoginClick}
                        id="btnLogin"
                        type="submit"
                        className="btn btn-primary"
                    >
                        Login
                    </button>
                </form>
            </div>
        );
    }
}

export default Login;
