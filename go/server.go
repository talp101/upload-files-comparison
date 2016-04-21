package main

import (
	"github.com/go-martini/martini"
	"net/http"
  "io"
	"os"
)

func main() {
	m := martini.Classic()
	m.Post("/upload", func(r *http.Request) (int, string) {
		err := r.ParseMultipartForm(100000)
		if err != nil {
			return http.StatusInternalServerError, err.Error()
		}
		files := r.MultipartForm.File["files"]
		file, err := files[0].Open()
		defer file.Close()
		if err != nil {
			return http.StatusInternalServerError, err.Error()
		}
		dst, err := os.Create("./uploads/" + files[0].Filename)
		defer dst.Close()
		if err != nil {
			return http.StatusInternalServerError, err.Error()
		}
    if _, err := io.Copy(dst, file); err != nil {
			return http.StatusInternalServerError, err.Error()
		}

		return 204, "ok"
	})

	m.Run()
}
