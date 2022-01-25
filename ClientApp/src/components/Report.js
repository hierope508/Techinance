import React, { Component } from 'react';

export class Report extends Component {

    constructor(props) {
        super(props);
        this.state = { rows: [], loading: true };
        this.id = props.id;
        this.name = props.name;
    }


    componentDidMount() {
        this.getRows(this.id);
    }

    static renderReportTable(rows) {

        let columns = Object.getOwnPropertyNames(rows[0]);
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        {columns.map(c =>
                            <th>{c}</th>
                        )}
                    </tr>
                </thead>
                <tbody>
                    {rows.map(r=>
                        <tr key={rows.indexOf(r)}>
                            {columns.map(c =>
                                <td key={rows.indexOf(r) + "_" + columns.indexOf(c)  }>{r[c].toString()}</td>
                            )}
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Report.renderReportTable(this.state.rows);

        return (
            <div>
                <h3 style={{ alignSelf: "center", textAlign: "center" }} id="tabelLabel" >{this.name}</h3>
                {contents}
            </div>
        );
    }

    async getRows(id) {
        const response = await fetch(`Report/Execute/${id}`);
        const data = await response.json();
        this.setState({ rows: data, loading: false });
    }
}
