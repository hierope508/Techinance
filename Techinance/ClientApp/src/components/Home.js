import React, { Component } from 'react';
import { Redirect } from "react-router";
import { checkIfLoggedIn } from "../Utils";
import { Report } from "./Report";

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { reports: [], loading: true };
    }

    componentDidMount() {
        this.getReports();
    }

    async getReports() {
        const response = await fetch('Report/GetAll');
        const data = await response.json();
        this.setState({ reports: data, loading: false });
    }

    render() {

        if (!checkIfLoggedIn()) {
            return <Redirect to="/Login" />;
        }

        return (

            <div>
                <h2 style={{ alignSelf: "center", textAlign: "center", marginBottom: "2vw" }}>Reports</h2>
                <div class="container" style={{ display: "flex", marginBottom: "5vw", alignContent: "space-between" }}>


                    {
                        this.state.loading ? <div></div> : this.state.reports.map(r => <div key={this.state.reports.indexOf(r) } style={{ maxWidth: "20vw", marginLeft: "2vw" }}> <Report id={r.id} name={r.name} /> </div>)
                    }

                </div>

            </div>

        );
    }
}
