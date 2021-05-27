const Header = () => {
    
        return (

            <header>
                <nav class="navbar navbar-expand-md navbar-dark bg-dark">
                    <a class="navbar-brand" href="/">SportsPro</a>
                    <button class="navbar-toggler" type="button"
                        data-toggle="collapse" data-target="#navbarSupportedContent"
                        aria-controls="navbarSupportedContent" aria-expanded="false"
                        aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <nav class="collapse navbar-collapse" id="navbarSupportedContent">
                        <div class="navbar-nav mr-auto">
                            <a class="nav-item nav-link active" asp-controller="Home" asp-action="List" href="/">Home</a>
                            <a class="nav-item nav-link active" asp-controller="Products" asp-action="List" href="/products">Products</a>
                            <a class="nav-item nav-link active" asp-controller="Technicians" asp-action="List" href="/technicians">Technicians</a>
                            <a class="nav-item nav-link active" asp-controller="Customers" asp-action="List" href="/customers">Customers</a>
                            <a class="nav-item nav-link active" asp-controller="Incidents" asp-action="List" href="/incidents">Incidents</a>
                            <a class="nav-item nav-link active" asp-controller="Registrations" asp-action="GetCustomer">Registrations</a>
                        </div>
                        <div class="navbar-nav navbar-right">
                            <a class="nav-item nav-link active" asp-controller="Home" asp-action="About">About</a>
                        </div>
                    </nav>
                </nav>
            </header>



        );
    
}

export default Header