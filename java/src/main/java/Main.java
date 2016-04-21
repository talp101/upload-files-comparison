import javax.servlet.MultipartConfigElement;
import javax.servlet.http.Part;

import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

import static spark.Spark.*;

public class Main {
    public static void main(String[] args) {
        post("/upload",(req,res)->{
            if (req.raw().getAttribute("org.eclipse.jetty.multipartConfig") == null) {
                MultipartConfigElement multipartConfigElement = new MultipartConfigElement(System.getProperty("java.io.tmpdir"));
                req.raw().setAttribute("org.eclipse.jetty.multipartConfig", multipartConfigElement);
            }
            Part file = req.raw().getPart("files");
            String filename = file.getSubmittedFileName();
            Path filePath = Paths.get("./uploads/" + filename);
            Files.copy(file.getInputStream(),filePath);
            halt(204);
            return "ok";
        });
    }

}
