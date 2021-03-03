Chat
==========
Ferramenta de comunicação desenvolvido em C#, utilizando WebSocket para comunicação entre cliente servidor, armazenamento em memória para controlar os registro de salas e usuários.
O chat deve ser utilizado por linha de comando de acordo as informações contidas na tabela a baixo, existem alguns comandos que estão indisponíveis no momento e que serão introduzidos nas próximas versões.
|Comando|Descrição|
|---|---|
|connect|realiza a conexão com o usuário a sala|
|list|retorna a lista de salas ou usuários conectados na sala|
|msgall|envia uma mensagem pública para todos os usuários da sala|
|public|envia uma mensagem pública para um usuário específico da sala|
|private|envia uma mensagem privada para um usuário específico da sala|
|createroom|cria uma sala|
|changeroom|muda o usuário de sala|
|exit|sai do chat|
|ping|envia requisições para o servidor para manter o usuário ativo (indisponível)|
----
Os próximos itens explicam como devem ser utilizados cada um dos comandos.
#### Connect
O comando **connect** adiciona o usuário na sala especificada e enviar uma mensagem todos os participantes da sala notificando a entrada. Para realizar a conexão com o servidor é necessário informar barra concatenado com a palavra **connect**, informar **{username}** referente ao nome do usuário e o **{roomname}** referente ao nome da sala.
```
/connect {username} {roomname}
```
**Exemplo:** Para conectar o usuário *mk-1* na sala global.
```
/connect mk-1 Global
```
#### List
Atualmente, é permitido obter a listagem de salas e usuários, sendo que, a listagem de usuários só é realizada quando o usuário estiver em alguma sala. Para realizar obter a listagem é necessário informar barra concatenado com a palavra **list** e informar **rooms** para retornar as salas ou **users** para retornar os usuários da sala.
```
/list [rooms|users]
```
**Exemplo:** Listar as salas disponíveis
```
/list rooms
```
#### MsgAll
O comando **msgall** permite o envio de uma mensagem para todos os usuários contidos na sala em que o emissor está localizado. Para enviar a mensagem é necessário informar barra concatenado com a palavra **msgall** e informar a mensagem no parâmetro **{message}**.
```
/msgall {message}
```
**Exemplo:** Enviar mensagem "*todo mundo da sala pode ver*" para todos os usuários da sala global.
```
/msgall todo mundo da sala pode ver
```
#### Public
O comando **public** permite o envio de uma mensagem direcionada a um usuário mas, todos os usuários contidos na sala em que o emissor está localizado podem visualizar. Para enviar a mensagem é necessário informar barra concatenado com a palavra **public**, o nome do usuário no parâmetro **{username}** e a mensagem deve ser informado no parâmetro **{message}**.
```
/public {username} {message}
```
**Exemplo:** Enviando a mensagem "*todo mundo da sala pode ver a mensagem para o mk-2*" para o usuário *mk-2* para todos os usuários da sala global lerem.
```
/public mk-2 todo mundo da sala pode ver a mensagem para o mk-2
```
#### Private
O comando **private** permite o envio de uma mensagem particular a um usuário. Sendo assim, os usuários contidos na sala em que o emissor está localizado não podem visualizar a mensagem, a não ser o usuário destinatário. Para enviar a mensagem é necessário informar barra concatenado com a palavra **private**, o nome do usuário destinatário no parâmetro **{username}** e a mensagem deve ser informado no parâmetro **{message}**.
```
/private {username} {message}
```
**Exemplo:** Enviando a mensagem "*somente para o usuário mk-2*" para somente o usuário *mk-2* visualizar.
```
/private mk-2 somente para o usuário mk-2
```
#### CreateRoom
Permite que o usuário crie novas salas para serem utilizadas por outros usuários conectados. Para criar a sala é necessário informar barra concatenado com a palavra **createroom** e o nome da sala no parâmetro **{roomname}**.
```
/createroom {roomname}
```
**Exemplo:** Criar a sala *DBA*.
```
/createroom DBA
```
#### ChangeRoom
Caso o usuário deseja mudar de sala, o comando **changeroom** realiza essa alteração. Ao executá-lo, uma notificação é enviada para os participantes da sala anterior, informando que o usuário saiu da sala e envia uma notificação para o participantes da nova sala, dizendo que o usuário acabou de entrar na sala. Para mudar a sala é necessário informar barra concatenado com a palavra **changeroom** e o nome da sala no parâmetro **{roomname}**.
```
/changeroom {roomname}
```
**Exemplo:** Mudar para a sala *devops*.
```
/changeroom devops
```
#### Exit
O comando **exit** remove o usuário da sala, notificando os outros participantes da sala que o usuário saiu e encerra a conexão com o servidor. Para executar o comando é necessário informar barra concatenado com a palavra **exit**.
```
/exit
```
**Exemplo:** Para sair e finalizar conexão.
```
/exit
```
#### Ping *(indisponível)*
O comando **ping** é utilizado para verificar a conexão entre cliente e servidor. Para executar o comando é necessário informar barra concatenado com a palavra. **ping**.
```
/ping
```
**Exemplo:** Para enviar requisições de ping no servidor.
```
/ping
```
----
Considerações
====
A aplicação foi desenvolvida utilizando C# 9.0 e .Net 5, utilizou-se Entity Framework para conexão com banco de dados, neste caso, foi utilizado o armzenamento de dados em memória e demonstrou alguns problemas durante a etapa de desenvolvimento, estes ocorriam ao atualizar e remover registros.
O problema encontrado:
> The instance of entity type 'User' cannot be tracked because another instance with the same key value for {'Id'} is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.

A solução encontrada foi:
````
protected virtual void Detach(TEntitentity, EntityState entityState)
{
    var local = dbSet.Local.FirstOrDefau(itm => itm.Id.Equals(entity.Id));
    if (local.Id != 0)
        context.Entry(local).State EntityState.Detached;
    context.Entry(entity).State entityState;
}
````
Executando esse métodos antes do **SaveChangesAsync**, permite que os dados armazenados possam ser atualizados e removidos. Desta forma, na hora de executar, defina o valor de **EntityState** para **Modified** e **Deleted** ao atualizar e remover.

Próximos passos seriam, remover o armazenamento em memória e utilizar um banco de dados, e melhorar a forma de comunicação, talvez trafegar com binário entre as duas pontas.