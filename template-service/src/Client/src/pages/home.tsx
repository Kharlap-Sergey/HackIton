import { Link } from "react-router-dom";
import { Button } from "@/components/ui/button";

const HomePage = () => (
  <div className="flex items-center justify-center min-h-screen flex-col items-center">
    <h2 className="scroll-m-20 pb-2 text-3xl font-semibold tracking-tight first:mt-0 mb-2">
      HOME PAGE
    </h2>
    <Button>
      <Link to="/test">Test Page</Link>
    </Button>
  </div>
);

export default HomePage;
