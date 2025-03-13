Небольшое приложение, позволяет регистрировать пользователей, писать отзывы. Есть админ, который может удалять чужие отзывы, в проекте присутствует ajax прокрутка отзывов. Используются EF Core и Identity Core для регистрации и авторизации пользователей и использования ролей.
Для регистрации требования к паролю: 
options.Password.RequireDigit = true; // Требовать хотя бы одну цифру
options.Password.RequireLowercase = true; // Требовать хотя бы одну строчную букву
options.Password.RequireUppercase = true; // Требовать хотя бы одну заглавную букву
options.Password.RequireNonAlphanumeric = true; // Требовать хотя бы один специальный символ
options.Password.RequiredLength = 6; // Минимальная длина пароля
options.Password.RequiredUniqueChars = 1; // Минимальное количество уникальных символов

![image](https://github.com/user-attachments/assets/6ee8eb41-9708-4f16-b18c-a1631cc57603)
![image](https://github.com/user-attachments/assets/4f443c79-4d44-4ca8-b2cc-80d320e35084)

Админка на странице отзывов доступна только админу:
![image](https://github.com/user-attachments/assets/f5204aff-0bc1-47fc-b8c0-f58f544fd75b)

![image](https://github.com/user-attachments/assets/f98c3b5b-b078-496d-ab21-cfc1c329b30c)



