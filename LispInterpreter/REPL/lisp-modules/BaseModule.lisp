(define nil (quote ()))
(define and (lambda (a b) (if a b nil)))
(define or (lambda (a b) (if a a b)))

