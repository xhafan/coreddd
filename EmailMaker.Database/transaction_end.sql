    COMMIT TRAN
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
    BEGIN
        ROLLBACK TRAN
    END
     
    PRINT 'ErrorLine '; PRINT ERROR_LINE()
    PRINT 'ErrorNumber '; PRINT ERROR_NUMBER()
    PRINT 'ErrorSeverity '; PRINT ERROR_SEVERITY()
    PRINT 'ErrorState '; PRINT ERROR_STATE()
    PRINT 'ErrorProcedure '; PRINT ERROR_PROCEDURE()
    PRINT 'ErrorLine '; PRINT ERROR_LINE()
    PRINT 'ErrorMessage '; PRINT ERROR_MESSAGE()
END CATCH