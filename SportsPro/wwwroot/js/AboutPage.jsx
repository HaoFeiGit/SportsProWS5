import About from "./components/About.jsx";
import Footer from "./components/Footer.jsx";
import Header from "./components/Header.jsx";



const App = () => {
    
        return (
            <>
                <Header />
                <main role="main" class="pb-5">
                    <div className="Container">

                        
                        <About />
                        <h1>This page was built with React</h1>

                    </div>
                </main>
                <Footer />
            </>
        );
    
}




ReactDOM.render(<App />, document.getElementById('content'));