if not exists(select * from syscolumns where id=object_id('Account') and name='DateCreated')
Begin
	alter table Account add DateCreated DATETIME NOT NULL DEFAULT(GETDATE())
end



