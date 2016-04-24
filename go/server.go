package main

import (
	"github.com/go-martini/martini"
	"github.com/nu7hatch/gouuid"
	"encoding/json"
	"net/http"
  "io"
	"os"
)

func main() {
	m := martini.Classic()
	m.Post("/upload", func(req *http.Request) (int, string) {
		err := req.ParseMultipartForm(100000)
		if err != nil {
			return http.StatusInternalServerError, err.Error()
		}
		files := req.MultipartForm.File["files"]
		file, err := files[0].Open()
		defer file.Close()
		if err != nil {
			return http.StatusInternalServerError, err.Error()
		}
		fileName, err := uuid.NewV4()
		dst, err := os.Create("./uploads/" + fileName.String())
		defer dst.Close()
		if err != nil {
			return http.StatusInternalServerError, err.Error()
		}
    if _, err := io.Copy(dst, file); err != nil {
			return http.StatusInternalServerError, err.Error()
		}
		jsonResponse, err := json.Marshal(map[string]string{"file_name": fileName.String()})
		return 200,string(jsonResponse)
	})
	m.Run()
}
